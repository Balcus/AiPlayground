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
import AddCircleIcon from "@mui/icons-material/AddCircle";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

import { ScopeModel } from "../../api/Models/ScopeModel";
import { Scope } from "../Shared/Types/Scope";
import { TableHeader } from "../Common/TableHeader";
import { LoadingRow } from "../Common/LoadingRow";
import { EmptyTableRow } from "../Common/EmptyTableRow";
import { ScopesApiClient } from "../../api/Clients/ScopesApiClient";
import { AddScopePopup } from "../Scopes/AddScopePopup";
import { EditScopePopup } from "../Scopes/EditScopePopup";
import { DeleteScopePopup } from "../Scopes/DeleteScopePopup";

// fix change name functionallity

export const Scopes: FC = () => {
  const [scopes, setScopes] = useState<Scope[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const [openAddPopup, setOpenAddPopup] = useState(false);
  const handleOpenAddPopup = () => setOpenAddPopup(true);
  const handleCloseAddPopup = () => setOpenAddPopup(false);

  const [editableScope, setEditableScope] = useState<Scope>();
  const [openEditPopup, setOpenEditPopup] = useState(false);
  const handleOpenEditPopup = () => setOpenEditPopup(true);
  const handleCloseEditPopup = () => setOpenEditPopup(false);

  const [openDeletePopup, setOpenDeletePopup] = useState(false);
  const [deletableScope, setDeletableScope] = useState<Scope | null>(null);

  const handleOpenDeletePopup = (scope: Scope) => {
    setDeletableScope(scope);
    setOpenDeletePopup(true);
  };

  const handleCloseDeletePopup = () => {
    setOpenDeletePopup(false);
    setDeletableScope(null);
  };

  const columns = [
    { id: "id", label: "Id" },
    { id: "name", label: "Name" },
    { id: "actions", label: "Actions" },
  ];

  const fetchScopes = async () => {
    try {
      setIsLoading(true);
      const res = await ScopesApiClient.getAllAsync();
      const fetchedScopes = res.map((e: ScopeModel) => ({ ...e } as Scope));
      setScopes(fetchedScopes);
      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  const deleteScope = async (deletedScope: Scope) => {
    try {
      if (!deletedScope.id) {
        return;
      }
      await ScopesApiClient.deleteOneAsync(deletedScope.id);
      setScopes(scopes.filter((scope) => scope.id !== deletedScope.id));
    } catch (error: any) {
      console.log(error);
    }
  };

  const renderActions = (scope: Scope) => (
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
          setEditableScope(scope);
          handleOpenEditPopup();
        }}
      >
        <EditIcon fontSize="small" />
      </IconButton>
      <IconButton
        size="small"
        onClick={() => {
          handleOpenDeletePopup(scope);
        }}
      >
        <DeleteIcon fontSize="small" />
      </IconButton>
    </Box>
  );

  useEffect(() => {
    fetchScopes();
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
            Scopes
          </Typography>
          <Typography variant="body1" color="text.secondary">
            {scopes.length} Items
          </Typography>
        </Box>
        <IconButton onClick={handleOpenAddPopup}>
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
            {scopes && scopes.length ? (
              scopes.map((scope: Scope, index: number) => (
                <TableRow
                  key={index}
                  hover
                  sx={{ "&:last-child td": { borderBottom: 0 } }}
                >
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {scope.id}
                  </TableCell>
                  <TableCell align="center" sx={{ padding: "1rem 1rem" }}>
                    {scope.name}
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      padding: "1rem 1rem",
                      width: "150px",
                    }}
                  >
                    {renderActions(scope)}
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

      <AddScopePopup
        open={openAddPopup}
        onClose={handleCloseAddPopup}
        onEditing={(scope: Scope) => {
          setScopes([...scopes, scope]);
        }}
      />

      {editableScope && (
        <EditScopePopup
          open={openEditPopup}
          onClose={() => {
            setEditableScope(undefined);
            handleCloseEditPopup();
          }}
          onEditing={(updatedScope: Scope) => {
            setScopes(
              scopes.map((scope) =>
                scope.id === updatedScope.id ? updatedScope : scope
              )
            );
          }}
          editableScope={editableScope}
        />
      )}

      <DeleteScopePopup
        open={openDeletePopup}
        onClose={handleCloseDeletePopup}
        scope={deletableScope}
        onConfirm={async () => {
          if (deletableScope) {
            await deleteScope(deletableScope);
          }
          handleCloseDeletePopup();
        }}
      />
    </Box>
  );
};
