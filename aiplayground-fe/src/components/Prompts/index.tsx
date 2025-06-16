import {
  Box,
  IconButton,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography,
} from "@mui/material";
import { FC, useEffect, useState } from "react";
import "./Prompts.css";
import AddCircleIcon from "@mui/icons-material/AddCircle";
import { EmptyTableRow } from "../Common/EmptyTableRow";
import { LoadingRow } from "../Common/LoadingRow";
import { TableHeader } from "../Common/TableHeader";
import { Prompt } from "../Shared/Types/Prompt";
import { Delete } from "@mui/icons-material";
import { PromptsApiClient } from "../../api/Clients/PromptsApiclient";
import { PromptModel } from "../../api/Models/PromptModel";
import { DeletePopup } from "../Common/DeletePopup";
import { useNavigate } from "react-router-dom";
import SettingsIcon from "@mui/icons-material/Settings";
import { ExpandableText } from "../Common/ExpandableText";

export const Prompts: FC = () => {
  const navigate = useNavigate();
  const [prompts, setPrompts] = useState<Prompt[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [openDeletePopup, setOpenDeletePopup] = useState<boolean>(false);
  const [promptToDelete, setPromptToDelete] = useState<Prompt>();

  useEffect(() => {
    fetchPrompts();
  }, []);

  const renderActions = (prompt: Prompt) => {
    return (
      <Box
        sx={{
          display: "flex",
          gap: "8px",
          justifyContent: "center",
          width: "100%",
        }}
      >
        <IconButton
          size="small"
          onClick={() => {
            navigate(`view/${prompt.id}`);
          }}
        >
          <SettingsIcon fontSize="small" />
        </IconButton>
        <IconButton
          size="small"
          onClick={() => {
            setPromptToDelete(prompt);
            setOpenDeletePopup(true);
          }}
        >
          <Delete fontSize="small" />
        </IconButton>
      </Box>
    );
  };

  const handleCreatePrompt = () => {
    navigate("/prompts/create");
  };

  const columns = [
    {
      id: "name",
      label: "Name",
    },
    {
      id: "systemMessage",
      label: "System Message",
    },
    {
      id: "userMessage",
      label: "User Message",
    },
    {
      id: "expectedResult",
      label: "Expected Result",
    },
    {
      id: "actions",
      label: "Actions",
    },
  ];

  const fetchPrompts = async () => {
    try {
      setIsLoading(true);
      const res = await PromptsApiClient.getAllAsync();
      const fetchedPrompts = res.map((e: PromptModel) => ({ ...e } as Prompt));
      setPrompts(fetchedPrompts);
      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  const deletedPrompt = async (deletedPrompt: Prompt) => {
    try {
      if (!deletedPrompt.id) {
        return;
      }

      await PromptsApiClient.deleteOneAsync(deletedPrompt.id);
      setPrompts(prompts.filter((prompt) => prompt.id !== deletedPrompt.id));
    } catch (error: any) {
      console.log(error);
    }
  };

  return (
    <Box
      sx={{
        paddingLeft: "4rem",
        paddingRight: "6rem",
        paddingTop: "1.5rem",
        paddingBottom: "1.5rem",
      }}
    >
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: "16px",
          borderBottom: "1px solid #e0e0e0",
          paddingBottom: "16px",
        }}
      >
        <Box>
          <Typography variant="h5" component="h1" fontWeight="bold">
            Prompts
          </Typography>
          <Typography variant="body1" color="text.secondary">
            {prompts.length} Items
          </Typography>
        </Box>
        <IconButton onClick={handleCreatePrompt}>
          <AddCircleIcon />
        </IconButton>
      </Box>

      <TableContainer
        component={Paper}
        sx={{
          boxShadow: "none",
          border: "1px solid #e0e0e0",
          borderRadius: "4px",
        }}
      >
        <Table>
          <TableHeader columns={columns} />
          <TableBody>
            {prompts && prompts.length ? (
              prompts.map((prompt: Prompt, index: number) => (
                <TableRow
                  key={index}
                  hover
                  sx={{ "&:last-child td": { borderBottom: 0 } }}
                >
                  <TableCell align="center">
                    <ExpandableText text={prompt.name} maxLength={20} />
                  </TableCell>
                  <TableCell align="center">
                    <ExpandableText text={prompt.systemMsg} maxLength={20} />
                  </TableCell>
                  <TableCell align="center">
                    <ExpandableText text={prompt.userMessage} maxLength={20} />
                  </TableCell>
                  <TableCell align="center">
                    <ExpandableText
                      text={prompt.expectedResponse}
                      maxLength={20}
                    />
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      padding: "1rem 1rem",
                      width: "150px",
                    }}
                  >
                    {renderActions(prompt)}
                  </TableCell>
                </TableRow>
              ))
            ) : isLoading ? (
              <LoadingRow />
            ) : (
              <EmptyTableRow />
            )}
          </TableBody>
        </Table>
      </TableContainer>

      <DeletePopup
        entityTitle={promptToDelete?.name ?? "Unknown"}
        open={openDeletePopup}
        onClose={() => {
          setOpenDeletePopup(false);
          setPromptToDelete(undefined);
        }}
        onConfirm={() => {
          if (promptToDelete) {
            deletedPrompt(promptToDelete);
          }
          setOpenDeletePopup(false);
          setPromptToDelete(undefined);
        }}
      />
    </Box>
  );
};
