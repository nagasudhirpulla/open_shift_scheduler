import { IEmployee } from "./IEmployee";
export type IGroupedEmployee = {
    [key: string]: IEmployee[];
};
