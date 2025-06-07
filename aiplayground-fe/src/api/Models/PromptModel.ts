import { ModelBase } from "../Base/ModelBase";

export interface PromptModel extends ModelBase<number> {
    name: string;
    systemMsg: string;
    userMessage: string;
    expectedResponse: string;
}