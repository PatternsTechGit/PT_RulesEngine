import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountByX } from '../models/AccountByX';
import TransferRequest from '../models/TransferRequest';
import { TransferResult } from '../models/TransferResult';
import AccountsService from '../services/accounts.service';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-transfer-funds',
  templateUrl: './transfer-funds.component.html',
  styleUrls: ['./transfer-funds.component.css']
})

export class TransferFundsComponent implements OnInit {
  userId: string | undefined;
  fromAccount = new AccountByX();
  toAccount = new AccountByX();
  amount: number = 0;
  message: string;

  constructor(private route: ActivatedRoute,
    private accountsService: AccountsService,
    private transactionService: TransactionService) { }

  ngOnInit(): void {
    this.initialize();
    this.userId = 'aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3';
    this.accountsService
      .getAccountByUser(this.userId)
      .subscribe({
        next: (data) => {
          this.initializeFrom(data);
        },
        error: (error) => {
        },
      });
  }

  getToAccount() {
    this.accountsService
      .getAccountByAccountNumber(this.toAccount.accountNumber)
      .subscribe({
        next: (data) => {
          this.initializeTo(data);
        },
        error: (error) => {
        },
      });
  }

  initializeToAccountDefault() {
    this.toAccount = new AccountByX();
    this.toAccount.accountNumber = "XXXX-XXXX"
    this.toAccount.accountTitle = "Unknown"
    this.toAccount.currentBalance = 0;
    this.toAccount.userImageUrl = '../../assets/images/noprofile.png'
  }

  initializeTo(data: any) {
    this.toAccount = new AccountByX();
    this.toAccount.accountNumber = data.accountNumber
    this.toAccount.accountTitle = data.accountTitle
    this.toAccount.currentBalance = data.currentBalance
    this.toAccount.userImageUrl = '../../assets/images/nancy.jpg'
  }

  initializeFrom(data: any) {
    this.fromAccount = new AccountByX();
    this.fromAccount.accountNumber = data.accountNumber
    this.fromAccount.accountTitle = data.accountTitle
    this.fromAccount.currentBalance = data.currentBalance
    this.fromAccount.userImageUrl = '../../assets/images/profile.jpg'
  }

  initialize() {
    this.amount = 0;
    this.initializeToAccountDefault();
  }
  transfer() {
    const transferRequest: TransferRequest = {
      accountFromId: this.fromAccount.accountNumber,
      accountToId: this.toAccount.accountNumber,
      amount: this.amount
    };
    this.transactionService
      .transfer(transferRequest)
      .subscribe({
        next: (result: TransferResult) => {
          if (result.isSuccess) {
            this.fromAccount.currentBalance -= this.amount;
            this.toAccount.currentBalance += this.amount;
            this.amount = 0;
            this.message = ''
          } else {
            this.message = result.errors
          }
        },
        error: (err) => {
        },
      });
  }
}
