export class Transaction {
    id: string;
    createdAt: Date;
    value: number;
    transactionOperation: string;
    operationType: string;
    previousBalance: number;
}