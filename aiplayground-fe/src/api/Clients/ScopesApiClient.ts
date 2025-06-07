import { AiPlaygroundApiClient } from "../Base/BaseApiClient";
import { ScopeModel } from "../Models/ScopeModel"

export const ScopesApiClient = {
    urlPath: "Scopes",

    getAllAsync(): Promise<ScopeModel[]> {
        return AiPlaygroundApiClient
            .get<ScopeModel[]>(this.urlPath)
            .then(response => response.data);
    },

    getOneAsync(id: number): Promise<ScopeModel> {
        return AiPlaygroundApiClient
            .get<ScopeModel>(this.urlPath + "/" + id)
            .then(response => response.data);
    },

    createOneAsync(scope: ScopeModel): Promise<ScopeModel> {  
        return AiPlaygroundApiClient
            .post(this.urlPath, scope)
            .then(response => response.data);
    },

    updateOneAsync(scope: ScopeModel): Promise<ScopeModel> {
        return AiPlaygroundApiClient
            .put(this.urlPath + "/" + scope.id, scope)
            .then(response => response.data)
    },

    deleteOneAsync(id: number): Promise<ScopeModel> {
        return AiPlaygroundApiClient
            .delete(this.urlPath + "/" + id)
            .then(response => response.data)
    },
}