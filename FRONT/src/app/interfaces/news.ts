import { Topics } from "./Topics";

export interface news {
    id: number;
    title: string;
    introduction: string;
    author: string;
    description: string;
    content: string;
    createdAt: string;    
    imageUrl: string;
    topics: Topics[]; 
    likes?: number;
}
