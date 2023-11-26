export enum UserRole {
  Dispatcher = 1,
  Admin = 2
}

export const UserRoleDisplay: Record<UserRole, string> = {
  [UserRole.Dispatcher]: 'Диспетчер',
  [UserRole.Admin]: 'Администратор'
};
