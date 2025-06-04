import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
} from "@mui/material";
import { FC } from "react";
import { Scope } from "../../Shared/Types/Scope";

interface DeleteScopePopupProps {
  open: boolean;
  onClose: () => void;
  onConfirm: () => void;
  scope: Scope | null;
}

export const DeleteScopePopup: FC<DeleteScopePopupProps> = ({
  open,
  onClose,
  onConfirm,
  scope,
}) => {
  if (!scope) return null;

  return (
    <Dialog
      open={open}
      onClose={onClose}
      aria-labelledby="delete-dialog-title"
      aria-describedby="delete-dialog-description"
    >
      <DialogTitle id="delete-dialog-title">Confirm Deletion</DialogTitle>
      <DialogContent>
        <DialogContentText id="delete-dialog-description">
          Are you sure you want to delete scope <strong>{scope.name}</strong>?
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button
          autoFocus
          onClick={onClose}
          color="primary"
          variant="contained"
          className="cancle-button"
        >
          Cancel
        </Button>
        <Button
          onClick={onConfirm}
          variant="contained"
          sx={{
            backgroundColor: "#5555",
            "&:hover": {
              backgroundColor: "#d32f2f",
            },
          }}
        >
          Delete
        </Button>
      </DialogActions>
    </Dialog>
  );
};
