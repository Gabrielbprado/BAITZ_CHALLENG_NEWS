import { Topics } from "./Topics"

export interface RegisterNews
{
    title: string;
    introduction: string;
    author: string;
    description: string;
    content: string;
    createdAt: string;    
    Topics: Array<{ Description: string }>; 
}