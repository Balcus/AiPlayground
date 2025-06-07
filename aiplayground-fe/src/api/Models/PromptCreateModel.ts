export interface PromptCreateModel {
    scopeId: number;
    name: string;
    systemMsg: string;
    userMessage: string;
    expectedResponse: string;
}