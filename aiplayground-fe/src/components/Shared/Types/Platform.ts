import { Model } from "./Model";

export type Platform = {
    name?:string,
    imageUrl?:string,
    models: Model[],
    id?: number,
}