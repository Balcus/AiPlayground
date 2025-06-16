import { ModelBase } from "../Base/ModelBase";
import { ModelModel } from "./ModelModel";
import { PromptModel } from "./PromptModel";

export interface RunGetModel extends ModelBase<number> {
  model: ModelModel;
  prompt: PromptModel;
  actualResponse: string;
  temperature: number;
  rating: number;
  userRating: number;
}