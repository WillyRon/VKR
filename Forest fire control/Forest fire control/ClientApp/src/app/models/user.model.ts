import { UserRole } from "./emums/user-role.enum";

export interface User {
  email: string;
  name?: string;
  lastName?: string;
  middleName?: string;
  phoneNumber?: string;
  region: string;
  role: UserRole;
}