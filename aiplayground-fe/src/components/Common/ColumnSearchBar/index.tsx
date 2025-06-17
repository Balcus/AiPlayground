import { FC } from "react";
import {
  Box,
  TextField,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
  SelectChangeEvent,
} from "@mui/material";

export interface SearchColumn {
  id: string;
  label: string;
  searchable?: boolean;
}

interface SearchBarProps {
  columns: SearchColumn[];
  selectedColumn: string;
  searchValue: string;
  onColumnChange: (column: string) => void;
  onSearchChange: (value: string) => void;
  placeholder?: string;
}

export const SearchBar: FC<SearchBarProps> = ({
  columns,
  selectedColumn,
  searchValue,
  onColumnChange,
  onSearchChange,
  placeholder = "Search...",
}) => {
  const handleColumnChange = (event: SelectChangeEvent) => {
    onColumnChange(event.target.value);
  };

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    onSearchChange(event.target.value);
  };

  const searchableColumns = columns.filter(
    (col) => col.searchable !== false && col.id !== "actions"
  );

  return (
    <Box
      sx={{
        display: "flex",
        gap: 2,
        alignItems: "center",
        marginBottom: 2,
        maxWidth: "100%",
      }}
    >
      <FormControl sx={{ minWidth: 150 }}>
        <InputLabel>Search by</InputLabel>
        <Select
          value={selectedColumn}
          label="Search by"
          onChange={handleColumnChange}
        >
          {searchableColumns.map((column) => (
            <MenuItem key={column.id} value={column.id}>
              {column.label}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      <TextField
        fullWidth
        label={placeholder}
        value={searchValue}
        onChange={handleSearchChange}
        variant="outlined"
        sx={{ flexGrow: 1 }}
      />
    </Box>
  );
};
