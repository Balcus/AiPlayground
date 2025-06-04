import { ModelBase } from "../Base/ModelBase";

export interface ModelModel extends ModelBase<number> {
    name?: string,
    averageRating: number,
}