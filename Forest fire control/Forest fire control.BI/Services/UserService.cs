﻿using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.Data.Model;
using Forest_fire_control.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using Forest_fire_control.Data.Config;
using Microsoft.EntityFrameworkCore;
using Forest_fire_control.Data.Entity;
using Forest_fire_control.Data.Enums;

namespace Forest_fire_control.BI.Services
{
    public class UserService : IUserService
    {

        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;

        public UserService(IConfiguration configuration, IEmailSender emailSender, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }

        public async Task<AuthenticationResult> Login(LoginModel model)
        {
            var user = await GetUser(model.Email);
            var result = new AuthenticationResult();

            if (user != null)
            {
                var hashPassword = HashPassword(model.Password);
                var signInResult = VerifyPassword(hashPassword, user.Password);

                if (signInResult)
                {
                    var token = GenerateJwtToken(user);
                    result.Success = true;
                    result.Token = token;
                }
                else
                {
                    result.ErrorMessage = "Неверный пароль";
                }
            }
            else
            {
                result.ErrorMessage = "Пользователь не найден";
            }

            return result;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthenticationResult> CreateUser(UserModel userModel, Guid regionId)
        {
            var result = new AuthenticationResult();
            var userDb = await GetUser(userModel.Email);
            if(userDb != null)
            {
                result.ErrorMessage = "Пользователь с таким email существует";
                return result;
            }
            
            string password = GenerateRandomPassword();
            var hashPassword = HashPassword(password);
            var user = new User
            {
                Email = userModel.Email,
                Role = userModel.Role,
                Name = userModel.Name,
                Password = hashPassword,
                PhoneNumber = userModel.PhoneNumber,
                LastName = userModel.LastName,
                MiddleName = userModel.MiddleName,
                RegionId = regionId
            };
            _dbContext.User.Add(user);
            var saveChangesResult = await _dbContext.SaveChangesAsync();

            if (saveChangesResult > 0)
            {
                // Отправка пароля на почту
                await _emailSender.SendEmailAsync(user.Email, "Ваш новый пароль", $"Ваш пароль: {password}");

                result.Success = true;
                return result;
            }
            else
            {
                result.ErrorMessage = "Пользователь не создан";
                return result;
            }
          
        }

        public async Task<AuthenticationResult> CreateApplication(ApplicationModel applicationModel, Guid? observationId)
        {
            var result = new AuthenticationResult();
            var user = await GetUser(applicationModel.UserEmail);
            var application = new Application
            {
                UserId = user.Id,
                ObservationSiteId = observationId,
                Data = applicationModel.Data,
                Description = applicationModel.Description,
                Status = applicationModel.Status
            };
            _dbContext.Application.Add(application);
            var saveChangesResult = await _dbContext.SaveChangesAsync();

            if (saveChangesResult > 0)
            {
                result.Success = true;
                return result;
            }
            else
            {
                result.ErrorMessage = "Ошибка при создании заявки";
                return result;
            }

        }

        public async Task<List<ApplicationModel>> GetApplications()
        {
            var applications = await _dbContext.Application
                .Include(a => a.User)
                .Include(a => a.ObservationSite)
                    .ThenInclude(o => o.Region)
                .ToListAsync();

            var applicationModels = new List<ApplicationModel>();

            foreach (var application in applications)
            {
                var applicationModel = new ApplicationModel
                {
                    UserEmail = application.User.Email,
                    ObservationSite = MapObservationSiteToModel(application.ObservationSite),
                    Data = application.Data,
                    Description = application.Description,
                    Status = application.Status
                };

                applicationModels.Add(applicationModel);
            }
            applicationModels.OrderBy(a=>a.Status).ToList();
            return applicationModels;
        }

        public async Task<AuthenticationResult> ChangeApplicationStatus(ApplicationModel applicationModel)
        {
            var result = new AuthenticationResult();
            var user = await GetUser(applicationModel.UserEmail);
            var application = await _dbContext.Application
              .FirstOrDefaultAsync(a => a.UserId == user.Id && a.Data == applicationModel.Data);

            if (application != null)
            {
                application.Status = IncedentStatusEnum.Done;

                var saveChangesResult = await _dbContext.SaveChangesAsync();

                if (saveChangesResult > 0)
                {
                    result.Success = true;
                    return result;
                }
                
            }

            result.ErrorMessage = "Не удалось найти или обновить заявку.";
            return result;
        }

        private ObservationSiteModel MapObservationSiteToModel(ObservationSite observation)
        {
            if (observation == null)
            {
                return null;
            }

            return new ObservationSiteModel
            {
                Name = observation.Name,
                Longitude = observation.Longitude,
                Latitude = observation.Latitude,
                Address = observation.Address,
                Region = observation.Region?.Name,
                Url = observation.Url,
            };
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 12)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
        private bool VerifyPassword(string hashedPassword, string password)
        {
            return hashedPassword.Equals(password, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<User> GetUser(string email)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
