import { FC, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "./ManagePrompt.css";
import {
  Box,
  Button,
  CircularProgress,
  MenuItem,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { ScopesApiClient } from "../../../api/Clients/ScopesApiClient";
import { ScopeModel } from "../../../api/Models/ScopeModel";
import { Prompt } from "../../Shared/Types/Prompt";
import { PromptCreate } from "../../Shared/Types/PromptCreate";
import { Scope } from "../../Shared/Types/Scope";
import { PromptsApiClient } from "../../../api/Clients/PromptsApiclient";
import { RunConfig } from "../RunConfig";
import { PromptCreateModel } from "../../../api/Models/PromptCreateModel";

const DEFAULT_PROMPT: Prompt = {
  id: undefined,
  name: undefined,
  systemMsg: undefined,
  userMessage: undefined,
  expectedResponse: undefined,
};

export const ManagePrompt: FC = () => {
  const navigate = useNavigate();
  const [prompt, setPrompt] = useState<Prompt | PromptCreate>(DEFAULT_PROMPT);
  const [scopes, setScopes] = useState<Scope[]>([]);
  const [areScopesLoading, setAreScopesLoading] = useState(false);
  const [errors, setErrors] = useState<{ [key: string]: string }>({});
  const [promptLoading, setPromptLoading] = useState(false);
  const { id } = useParams();

  const fetchScopes = async () => {
    try {
      setAreScopesLoading(true);
      const res = await ScopesApiClient.getAllAsync();
      const fetchedScopes = res.map((e: ScopeModel) => ({ ...e } as Scope));
      setScopes(fetchedScopes);
      setAreScopesLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  const fetchPrompt = async (promptId: string) => {
    try {
      setPromptLoading(true);
      const response = await PromptsApiClient.getOneAsync(parseInt(promptId));
      if (response) {
        setPrompt(response as Prompt);
      }
      setPromptLoading(false);
    } catch (error) {
      console.error("Error fetching prompt:", error);
    }
  };

  useEffect(() => {
    if (id) {
      fetchPrompt(id);
    } else {
      fetchScopes();
    }
  }, []);

  const computeTitle = () => {
    if (id && prompt) {
      return (
        <>
          <h1>
            View prompt: <strong>{prompt.name}</strong> 🧐
          </h1>
        </>
      );
    }
    return <h1>Create new prompt 👷‍♂️🧰</h1>;
  };

  const isFormValid = () => {
    const hasName = prompt.name && prompt.name.trim().length > 0;
    const hasSystemMsg = prompt.systemMsg && prompt.systemMsg.trim().length > 0;
    const hasUserMessage =
      prompt.userMessage && prompt.userMessage.trim().length > 0;
    const hasExpectedResponse =
      prompt.expectedResponse && prompt.expectedResponse.trim().length > 0;

    if (!id) {
      const scopeId = (prompt as PromptCreate).scopeId;
      const hasScopeId = typeof scopeId === "number" && scopeId > 0;
      return (
        hasName &&
        hasSystemMsg &&
        hasUserMessage &&
        hasExpectedResponse &&
        hasScopeId
      );
    }

    return hasName && hasSystemMsg && hasUserMessage && hasExpectedResponse;
  };

  const validateField = (name: string, value: string) => {
    const isEmpty = (val: string) =>
      val === null || val === undefined || String(val).trim() === "";
    switch (name) {
      case "name":
        return isEmpty(value) ? "Name is required" : "";
      case "scopeId":
        return isEmpty(value) ? "Scope is required" : "";
      case "systemMsg":
        return isEmpty(value) ? "systemMsg is required" : "";
      case "userMessage":
        return isEmpty(value) ? "userMessage is required" : "";
      case "expectedResponse":
        return isEmpty(value) ? "expectedResponse is required" : "";
      default:
        return "";
    }
  };

  const handleSave = async () => {
    if (!isFormValid()) {
      return;
    }

    try {
      await PromptsApiClient.createOneAsync(prompt as PromptCreateModel);
      navigate("/prompts");
    } catch (error: any) {
      console.log("Error saving prompt:", error);
    }
  };

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = event.target;
    setPrompt((prevPrompt) => {
      const updatedPrompt = { ...prevPrompt, [name]: value };
      const fieldError = validateField(name, value);
      setErrors((prevErrors) => ({
        ...prevErrors,
        [name]: fieldError,
      }));
      return updatedPrompt;
    });
  };

  const handleCancel = () => {
    navigate("/prompts");
  };

  if (areScopesLoading) {
    return (
      <Stack justifyContent="center" alignItems="center" height="80vh">
        <CircularProgress size={60} />
      </Stack>
    );
  }

  const validateForm = (): boolean => {
    const requiredFields = [
      "name",
      "systemMsg",
      "userMessage",
      "expectedResult",
      "scopeId",
    ];

    const newErrors: { [key: string]: string } = {};

    let isValid = true;
    for (const field of requiredFields) {
      const value = (prompt as any)[field];
      const error = validateField(field, value);
      if (error) {
        newErrors[field] = error;
        isValid = false;
      }
    }

    setErrors(newErrors);
    return isValid;
  };

  return (
    <Box className="manage-prompt-wrapper">
      <div className="manage-prompt-form">
        <div className="manage-prompt-title">{computeTitle()}</div>

        <div className="form-field">
          <Typography variant="h6">Name</Typography>
          <TextField
            key="name"
            id="name"
            name="name"
            value={prompt.name}
            onChange={handleChange}
            required
            placeholder="Prompt Name"
            disabled={!!id}
          />
        </div>

        {!id && (
          <div className="form-field">
            <Typography variant="h6">Scope</Typography>
            <TextField
              select
              key="scopeId"
              id="scopeId"
              name="scopeId"
              value={(prompt as PromptCreate).scopeId || ""}
              onChange={handleChange}
              required
              placeholder="Scope"
            >
              {scopes.map((scope) => (
                <MenuItem key={scope.id} value={scope.id}>
                  {scope.name}
                </MenuItem>
              ))}
            </TextField>
          </div>
        )}

        <div className="form-field">
          <Typography variant="h6">System Message</Typography>
          <TextField
            key="systemMsg"
            id="systemMsg"
            name="systemMsg"
            value={prompt.systemMsg}
            multiline
            rows={1}
            onChange={handleChange}
            required
            placeholder="Instructions for Ai Agent"
            disabled={!!id}
          />
        </div>

        <div className="form-field">
          <Typography variant="h6">User Message</Typography>
          <TextField
            key="userMessage"
            id="userMessage"
            name="userMessage"
            value={prompt.userMessage}
            multiline
            rows={1}
            onChange={handleChange}
            required
            placeholder="Question"
            disabled={!!id}
          />
        </div>

        <div className="form-field">
          <Typography variant="h6">Expected Response</Typography>
          <TextField
            key="expectedResponse"
            id="expectedResponse"
            name="expectedResponse"
            value={prompt.expectedResponse}
            multiline
            rows={1}
            onChange={handleChange}
            required
            placeholder="Expected Response"
            disabled={!!id}
          />
        </div>

        {!id && (
          <div className="button-container">
            <Button variant="outlined" size="large" onClick={handleCancel}>
              Cancel
            </Button>
            <Button
              variant="contained"
              size="large"
              onClick={handleSave}
              disabled={!isFormValid()}
            >
              Save Prompt
            </Button>
          </div>
        )}
      </div>

      {id && (
        <div className="run-config-wrapper">
          <Typography variant="h6">Run Section</Typography>
          <RunConfig />
        </div>
      )}
    </Box>
  );
};
