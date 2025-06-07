import { AiPlaygroundApiClient } from "../Base/BaseApiClient";
import { PromptCreateModel } from "../Models/PromptCreateModel";
import { PromptModel } from "../Models/PromptModel"

export const PromptsApiClient = {
  urlPath: "Prompts",

  async getAllAsync(): Promise<PromptModel[]> {
    return AiPlaygroundApiClient.get<PromptModel[]>(this.urlPath).then(
      (response) => response.data
    );
  },

  async getOneAsync(id: number): Promise<PromptModel> {
    return AiPlaygroundApiClient.get<PromptModel>(this.urlPath + "/" + id).then(
      (response) => response.data
    );
  },

  async createOneAsync(model: PromptCreateModel): Promise<PromptModel> {
    return AiPlaygroundApiClient.post<PromptModel>(this.urlPath, model).then(
      (response) => response.data
    );
  },

  async deleteOneAsync(id: number): Promise<void> {
    return AiPlaygroundApiClient.delete(this.urlPath + "/" + id).then(
      (response) => response.data
    );
  },
};