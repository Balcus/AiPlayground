import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { Box, Tooltip, Collapse, IconButton } from "@mui/material";
import { FC, useState } from "react";

interface Props {
  text?: string;
  maxLength: number;
}

export const ExpandableText: FC<Props> = ({ text, maxLength }) => {
  const [isExpanded, setIsExpanded] = useState<boolean>(false);
  const shouldTruncate = text && text.length > maxLength;
  const truncatedText = shouldTruncate
    ? `${text.substring(0, maxLength)}...`
    : text;

  const handleToggle = () => {
    setIsExpanded(!isExpanded);
  };

  if (!shouldTruncate) {
    return <>{text}</>;
  }

  return (
    <Box
      sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
    >
      <Tooltip title={isExpanded ? "" : text} placement="top">
        <Box sx={{ maxWidth: isExpanded ? "none" : "200px" }}>
          <Collapse in={isExpanded} collapsedSize="1.2em">
            <Box
              sx={{
                wordBreak: "break-word",
                whiteSpace: isExpanded ? "pre-wrap" : "nowrap",
                overflow: isExpanded ? "visible" : "hidden",
                textOverflow: isExpanded ? "clip" : "ellipsis",
              }}
            >
              {isExpanded ? text : truncatedText}
            </Box>
          </Collapse>
        </Box>
      </Tooltip>
      <IconButton size="small" onClick={handleToggle} sx={{ ml: 1 }}>
        {isExpanded ? <ExpandLess /> : <ExpandMore />}
      </IconButton>
    </Box>
  );
};
