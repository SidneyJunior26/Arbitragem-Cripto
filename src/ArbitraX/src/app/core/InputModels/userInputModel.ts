export interface NewUserInputModel {
  name: string;
  email: string;
  password: string;
  trial: boolean;
}

export interface UpdateUserInputModel {
  name: string;
  email: string;
  trial: boolean;
}
