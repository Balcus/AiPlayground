import {
  Box,
  IconButton,
  Paper,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
} from "@mui/material";
import { FC, useEffect, useState } from "react";
import "./Prompts.css";
import AddCircle from "@mui/icons-material/AddCircle";
import { EmptyTableRow } from "../Common/EmptyTableRow";
import { LoadingRow } from "../Common/LoadingRow";
import { TableHeader } from "../Common/TableHeader";
import { renderLabelDisplayedRows } from "../Shared/Utils/table.util";
import { Prompt } from "../Shared/Types/Prompt";
import { Delete } from "@mui/icons-material";
import { PromptsApiClient } from "../../api/Clients/PromptsApiclient";
import { PromptModel } from "../../api/Models/PromptModel";
import { DeletePopup } from "../Common/DeletePopup";
import { useNavigate } from "react-router-dom";

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
      <>
        <IconButton
          onClick={() => {
            setPromptToDelete(prompt);
            setOpenDeletePopup(true);
          }}
        >
          <Delete color="primary" fontSize="large" />
        </IconButton>
      </>
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
    <>
      <Box className="prompts-wrapper">
        <Stack
          flexDirection={"row"}
          justifyContent={"center"}
          alignItems={"center"}
        >
          <Box className="prompts-title">Prompts</Box>
        </Stack>
        <Box>
          <Box>
            <IconButton onClick={handleCreatePrompt}>
              <AddCircle color="primary" fontSize="large" />
            </IconButton>
          </Box>
        </Box>
      </Box>

      <TableContainer component={Paper} className={"prompts-table-container"}>
        <Table>
          <TableHeader columns={columns} />
          <TableBody>
            {prompts && prompts.length ? (
              <>
                {prompts.map((prompt: Prompt, index: number) => (
                  <TableRow key={index} className={"prompts-table-row"}>
                    <TableCell align="center">{prompt.name}</TableCell>
                    <TableCell align="center">{prompt.systemMsg}</TableCell>
                    <TableCell align="center">{prompt.userMessage}</TableCell>
                    <TableCell align="center">
                      {prompt.expectedResponse}
                    </TableCell>
                    <TableCell align="center">
                      {renderActions(prompt)}
                    </TableCell>
                  </TableRow>
                ))}
              </>
            ) : isLoading ? (
              <LoadingRow />
            ) : (
              <EmptyTableRow />
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <Box className={"prompts-table-footer"}>
        {renderLabelDisplayedRows(prompts.length, "prompts")}
      </Box>
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
    </>
  );
};
