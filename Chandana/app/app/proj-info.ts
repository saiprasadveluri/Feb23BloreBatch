import { UserInfo } from "./user-info";

export interface ProjInfo {
    id:number;
    name:string;
    pm:string;
    managerId:number;
    qa: UserInfo[]; 
    developers: UserInfo[]; 
}
