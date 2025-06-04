import { ModelBase } from "../Base/ModelBase";
import { ModelModel } from "./ModelModel";

export interface PlatformModel extends ModelBase<number> {
    name?:string,
    imageUrl?:string,
    models: ModelModel[],
}