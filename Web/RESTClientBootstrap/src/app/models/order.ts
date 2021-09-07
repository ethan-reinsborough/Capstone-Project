import { OrderItem } from "./orderitem";

export class Order {
    public poNumber: Number;
    public poNumberFormatted: String;
    public statusID: Number;
    public status: String;
    public items: OrderItem[];
    public poCreationDate: Date;
    public poSubtotal: Number;
    public poTax: Number;
    public poTotal: Number;
    public employeeid: Number;
    public recordVersion: String;
    public employeeName: String;
}
