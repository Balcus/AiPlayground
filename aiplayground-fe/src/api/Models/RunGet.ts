import { Model } from "../../components/Shared/Types/Model";
import { Prompt } from "../../components/Shared/Types/Prompt";

export interface RunGet {
    id: number;
    model: Model;
    prompt: Prompt;
    actualResponse: string;
    temperature: number;
    rating: number;
    userRating: number;
}