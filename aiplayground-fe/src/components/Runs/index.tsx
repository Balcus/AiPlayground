import { FC, useEffect, useState, useMemo } from "react";
import "./Runs.css";
import { RunsApiClient } from "../../api/Clients/RunsApiClient";
import { ModelsApiClient } from "../../api/Clients/ModelsApiClient";
import {
  Box,
  Button,
  IconButton,
  Paper,
  Popover,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { Star } from "@mui/icons-material";
import { RunGetModel } from "../../api/Models/RunGetModel";
import { EmptyTableRow } from "../Common/EmptyTableRow";
import { LoadingRow } from "../Common/LoadingRow";
import { TableHeader } from "../Common/TableHeader";
import { RunGet } from "../Shared/Types/RunGet";
import { Prompt } from "../Shared/Types/Prompt";
import { Model } from "../Shared/Types/Model";
import { PromptsApiClient } from "../../api/Clients/PromptsApiclient";
import { SearchColumn, SearchBar } from "../Common/ColumnSearchBar";
import { ExpandableText } from "../Common/ExpandableText";

export const Runs: FC = () => {
  const [runs, setRuns] = useState<RunGet[]>([]);
  const [prompts, setPrompts] = useState<Prompt[]>([]);
  const [models, setModels] = useState<Model[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [ratingValue, setRatingValue] = useState<number | "">("");
  const [selectedRunId, setSelectedRunId] = useState<number | null>(null);
  const [selectedColumn, setSelectedColumn] = useState<string>("promptName");
  const [searchValue, setSearchValue] = useState<string>("");

  const columns: SearchColumn[] = [
    {
      id: "id",
      label: "Id",
    },
    {
      id: "promptName",
      label: "Prompt name",
    },
    {
      id: "expectedResult",
      label: "Expected result",
      searchable: false,
    },
    {
      id: "actualResult",
      label: "Actual result",
      searchable: false,
    },
    {
      id: "model",
      label: "Model",
    },
    {
      id: "rating",
      label: "Rating",
      searchable: false,
    },
    {
      id: "userRating",
      label: "User rating",
      searchable: false,
    },
    {
      id: "actions",
      label: "Actions",
      searchable: false,
    },
  ];

  const filteredRuns = useMemo(() => {
    if (!searchValue.trim()) {
      return runs;
    }

    const searchTerm = searchValue.toLowerCase().trim();

    return runs.filter((run) => {
      let fieldValue = "";

      switch (selectedColumn) {
        case "id":
          fieldValue = run.id?.toString() || "";
          break;
        case "promptName":
          fieldValue = run.prompt?.name || "";
          break;
        case "model":
          fieldValue = run.model?.name || "";
          break;
        default:
          return false;
      }

      const result = fieldValue.toLowerCase().includes(searchTerm);
      return result;
    });
  }, [runs, selectedColumn, searchValue]);

  const fetchPrompts = async (): Promise<Prompt[]> => {
    try {
      const res = await PromptsApiClient.getAllAsync();
      return res;
    } catch (error: any) {
      console.log("Error fetching prompts:", error);
      return [];
    }
  };

  const fetchModels = async (): Promise<Model[]> => {
    try {
      const res = await ModelsApiClient.getAllAsync();
      return res;
    } catch (error: any) {
      console.log("Error fetching models:", error);
      return [];
    }
  };

  const mapRunsWithPromptsAndModels = (
    runs: any[],
    prompts: Prompt[],
    models: Model[]
  ): RunGet[] => {
    return runs.map((run) => {
      const promptId = run.promptId || run.prompt?.id;
      const matchingPrompt = prompts.find((prompt) => prompt.id === promptId);

      const modelId = run.modelId || run.model?.id;
      const matchingModel = models.find((model) => model.id === modelId);

      return {
        ...run,
        prompt: matchingPrompt
          ? {
              id: matchingPrompt.id,
              name: matchingPrompt.name,
              expectedResponse: matchingPrompt.expectedResponse,
              systemMsg: matchingPrompt.systemMsg,
              userMessage: matchingPrompt.userMessage,
            }
          : null,
        model: matchingModel
          ? {
              id: matchingModel.id,
              name: matchingModel.name,
              averageRating: matchingModel.averageRating,
            }
          : null,
      } as RunGet;
    });
  };

  const fetchRuns = async () => {
    try {
      setIsLoading(true);

      const res = await RunsApiClient.getAllAsync();

      if (res.length > 0 && res[0].model && res[0].prompt) {
        const fetchedRuns = res.map((e: RunGetModel) => ({ ...e } as RunGet));
        setRuns(fetchedRuns);
      } else {
        const [promptsRes, modelsRes] = await Promise.all([
          fetchPrompts(),
          fetchModels(),
        ]);

        setPrompts(promptsRes);
        setModels(modelsRes);

        const mappedRuns = mapRunsWithPromptsAndModels(
          res,
          promptsRes,
          modelsRes
        );
        setRuns(mappedRuns);
      }

      setIsLoading(false);
    } catch (error: any) {
      console.log("Error fetching runs:", error);
      setIsLoading(false);
    }
  };

  const renderActions = (run: RunGet) => {
    const open = Boolean(anchorEl);

    const handleOpen = (
      event: React.MouseEvent<HTMLElement>,
      runId: number
    ) => {
      setAnchorEl(event.currentTarget);
      setSelectedRunId(runId);
    };

    const handleClose = () => {
      setAnchorEl(null);
      setRatingValue("");
      setSelectedRunId(null);
    };

    const handleGiveRating = async () => {
      if (
        selectedRunId != null &&
        ratingValue !== "" &&
        ratingValue >= 1 &&
        ratingValue <= 10
      ) {
        try {
          await RunsApiClient.rateAsync(selectedRunId, ratingValue);
          await fetchRuns();

          setRuns((prevRuns) =>
            prevRuns.map((r) =>
              r.id === selectedRunId ? { ...r, userRating: ratingValue } : r
            )
          );

          handleClose();
        } catch (error: any) {
          console.log("Error rating run:", error);
        }
      }
    };

    return (
      <>
        <IconButton onClick={(e) => handleOpen(e, run.id)}>
          <Star color="primary" fontSize="large" />
        </IconButton>
        <Popover
          open={open && selectedRunId === run.id}
          anchorEl={anchorEl}
          onClose={handleClose}
          anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
          transformOrigin={{ vertical: "top", horizontal: "center" }}
        >
          <Box
            sx={{
              p: 2,
              display: "flex",
              flexDirection: "column",
              gap: 1,
              width: 300,
            }}
          >
            <TextField
              label="Rating (1-10)"
              type="number"
              fullWidth
              value={ratingValue}
              onChange={(e) => setRatingValue(Number(e.target.value))}
              slotProps={{
                htmlInput: {
                  min: 1,
                  max: 10,
                },
              }}
            />
            <Button
              onClick={handleGiveRating}
              disabled={
                ratingValue === "" || ratingValue < 1 || ratingValue > 10
              }
            >
              Submit
            </Button>
          </Box>
        </Popover>
      </>
    );
  };

  useEffect(() => {
    fetchRuns();
  }, []);

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
            Runs
          </Typography>
          <Typography variant="body1" color="text.secondary">
            {filteredRuns.length} of {runs.length} Items
          </Typography>
        </Box>
      </Box>

      <SearchBar
        columns={columns}
        selectedColumn={selectedColumn}
        searchValue={searchValue}
        onColumnChange={setSelectedColumn}
        onSearchChange={setSearchValue}
        placeholder="Search runs..."
      />

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
            {filteredRuns && filteredRuns.length ? (
              filteredRuns.map((run: RunGet, index: number) => (
                <TableRow
                  key={index}
                  hover
                  sx={{ "&:last-child td": { borderBottom: 0 } }}
                >
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {run.id}
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {run.prompt?.name ?? ""}
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    <ExpandableText
                      text={run.prompt.expectedResponse}
                      maxLength={20}
                    />
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    <ExpandableText text={run.actualResponse} maxLength={20} />
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {run.model?.name ?? ""}
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {run.rating}
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {run.userRating}
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      padding: "1rem 1rem",
                      width: "150px",
                    }}
                  >
                    {renderActions(run)}
                  </TableCell>
                </TableRow>
              ))
            ) : isLoading ? (
              <LoadingRow />
            ) : searchValue ? (
              <TableRow>
                <TableCell colSpan={columns.length} align="center">
                  No results found for "{searchValue}" in{" "}
                  {columns.find((col) => col.id === selectedColumn)?.label}
                </TableCell>
              </TableRow>
            ) : (
              <EmptyTableRow />
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
};
