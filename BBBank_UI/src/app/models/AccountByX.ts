export class AccountByX {
  accountId: string;
  accountTitle: string;
  userImageUrl: string;
  currentBalance: number;
  accountStatus: string;
  accountNumber: string;
}

export interface GetAccountByXResponse {
  result: AccountByX
}