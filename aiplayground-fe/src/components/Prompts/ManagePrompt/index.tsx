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

const DEFAULT_PROMPT: PromptCreate = {
  scopeId: 0,
  name: "",
  systemMsg: "",
  userMessage: "",
  expectedResponse: "",
};

export const ManagePrompt: FC = () => {
  const navigate = useNavigate();
  const [prompt, setPrompt] = useState<Prompt | PromptCreate>(DEFAULT_PROMPT);
  const [scopes, setScopes] = useState<Scope[]>([]);
  const [areScopesLoading, setAreScopesLoading] = useState(false);
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

  const computeTitle = () => {
    if (id) {
      return (
        <>
          <h1>
            Manage prompt: <strong>{id}</strong>
          </h1>
        </>
      );
    }
    return <h1>Create new prompt 👷‍♂️</h1>;
  };

  const isFormValid = () => {
    const hasName = prompt.name && prompt.name.trim().length > 0;
    const hasSystemMsg = prompt.systemMsg && prompt.systemMsg.trim().length > 0;
    const hasUserMessage =
      prompt.userMessage && prompt.userMessage.trim().length > 0;
    const hasExpectedResponse =
      prompt.expectedResponse && prompt.expectedResponse.trim().length > 0;

    if (!id) {
      const hasScopeId = (prompt as PromptCreate).scopeId > 0;
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

  const handleSave = async () => {
    if (!isFormValid()) {
      return;
    }

    try {
      await PromptsApiClient.createOneAsync(prompt);
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
      return updatedPrompt;
    });
  };

  const handleCancel = () => {
    navigate("/prompts");
  };

  useEffect(() => {
    fetchScopes();
  }, []);

  if (areScopesLoading) {
    return (
      <Stack justifyContent="center" alignItems="center" height="80vh">
        <CircularProgress size={60} />
      </Stack>
    );
  }

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
    </Box>
  );
};
