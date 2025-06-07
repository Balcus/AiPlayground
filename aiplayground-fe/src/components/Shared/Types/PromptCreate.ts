export type PromptCreate = {
    scopeId: number;
    name: string;
    systemMsg: string;
    userMessage: string;
    expectedResponse: string;
};