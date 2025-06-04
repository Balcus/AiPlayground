import { AiPlaygroundApiClient } from "../Base/BaseApiClient"
import { PlatformModel } from "../Models/PlatformModel"

export const PlatfromsApiClient = {
    urlPath: "Platforms",

    getAllAsync(): Promise<PlatformModel[]> {
        return AiPlaygroundApiClient
            .get<PlatformModel[]>(this.urlPath)
            .then((response) => response.data);
    },

    getOneAsync(id: number): Promise<PlatformModel> {
        return AiPlaygroundApiClient
            .get<PlatformModel>(this.urlPath + "/" + id)
            .then((response) => response.data);
    },
}