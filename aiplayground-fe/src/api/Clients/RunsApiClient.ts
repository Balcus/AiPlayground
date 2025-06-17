import { AiPlaygroundApiClient } from "../Base/BaseApiClient";
import { RunCreateModel } from "../Models/RunCreateModel";
import { RunGetModel } from "../Models/RunGetModel";
import { RunModel } from "../Models/RunModel";

export const RunsApiClient = {
  urlPath: "Runs",

  async runAsync(model: RunCreateModel): Promise<RunModel[]> {
    return AiPlaygroundApiClient.post<RunModel[]>(this.urlPath, model).then(
      (response) => response.data
    );
  },

  async getAllAsync(): Promise<RunGetModel[]> {
    return AiPlaygroundApiClient.get<RunGetModel[]>(this.urlPath).then(
      (response) => response.data
    );
  },

  async rateAsync(id: number, userRating: number): Promise<void> {
    return AiPlaygroundApiClient.patch<void>(`${this.urlPath}/${id}`, {
      userRating,
    }).then((response) => response.data);
  },
};