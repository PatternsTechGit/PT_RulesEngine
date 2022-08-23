export class AccountListsResponse {
  accounts: Array<Account>;
  resultCount: number;
}

export class Account {
  id: string;
  accountTitle: string;
  user: User;
  currentBalance: number;
  accountStatus: string;
  accountNumber: string;
}

export class User {
  id: string;
  profilePicUrl: string;
  email: string;
  phoneNumber: string;
  firstName: string;
  lastName: string;
}

export interface AccountExistsResponse {
  result: boolean
}

export interface GetAccountResponse {
  result: Account
}