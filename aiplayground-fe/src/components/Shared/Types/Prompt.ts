export type Prompt = {
    id: number;
    scopeId: number;
    name: string;
    systemMsg: string;
    userMessage: string;
    expectedResponse: string;
};