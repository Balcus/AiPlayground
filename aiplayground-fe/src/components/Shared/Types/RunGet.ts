import { Model } from "./Model";
import { Prompt } from "./Prompt";

export interface RunGet {
  // TODO currently we do not have the Prompt and Model only their ids from the BackupOutlined, need to somehow map them to this
  id: number;
  model: Model;
  prompt: Prompt;
  actualResponse: string;
  temperature: number;
  rating: number;
  userRating: number;
}