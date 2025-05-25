import {
  Box,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Stack
} from "@mui/material";
import { FC, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import MenuIcon from "@mui/icons-material/Menu";
import HomeOutlinedIcon from '@mui/icons-material/HomeOutlined';
import DevicesIcon from "@mui/icons-material/Devices";
import SmartToyOutlinedIcon from '@mui/icons-material/SmartToyOutlined';
import FolderCopyOutlinedIcon from '@mui/icons-material/FolderCopyOutlined';
import WysiwygRoundedIcon from "@mui/icons-material/WysiwygRounded";
import SpeakerNotesOutlinedIcon from '@mui/icons-material/SpeakerNotesOutlined';
import "./Menu.css";

export const Menu: FC = () => {
    const [open, setOpen] = useState(false);
    const navigate = useNavigate();
    const location = useLocation();

    const toggleDrawer = (state: boolean) => () => {
        setOpen(state);
    };

    const isActive = (path: string) => location.pathname === path;

    const menuItems = [
        { key: "home", path: "/", label: "Home", icon: HomeOutlinedIcon },
        { key: "platforms", path: "/platforms", label: "Platforms", icon: DevicesIcon },
        { key: "models", path: "/models", label: "Models", icon: SmartToyOutlinedIcon },
        { key: "runs", path: "/runs", label: "Runs", icon: WysiwygRoundedIcon },
        { key: "scopes", path: "/scopes", label: "Scopes", icon: FolderCopyOutlinedIcon },
        { key: "prompts", path: "/prompts", label: "Prompts", icon: SpeakerNotesOutlinedIcon }
    ];

    return (
        <>
            {!open && (
                <IconButton onClick={toggleDrawer(true)} sx={{ position: "absolute", top: 16, left: 16, zIndex: 1300 }}>
                    <MenuIcon sx={{ color: "primary.main", fontSize: "2rem"}} />
                </IconButton>
            )}

            <Drawer anchor="left" open={open} onClose={toggleDrawer(false)} className="menu-drawer">
                <Box
                    className="menu-container"
                    bgcolor="secondary.light"
                    sx={{ width: 250 }}
                    role="presentation"
                    onClick={toggleDrawer(false)}
                    onKeyDown={toggleDrawer(false)}
                >
                    <Stack
                        flexDirection="row"
                        alignItems="center"
                        justifyContent="center"
                        p={2}
                    >
                        <img
                            src="./assets/robo.png"
                            className="menu-logo-image"
                            alt="AI Playground Logo"
                        />
                        <Box className="menu-title">
                            <Box component="span" sx={{ color: "primary.main" }}>
                                Ai
                            </Box>
                            <Box component="span" sx={{ color: "primary.dark" }}>
                                Playground
                            </Box>
                        </Box>
                    </Stack>

                    <List disablePadding>
                        {menuItems.map(({ key, path, label, icon: Icon }) => (
                            <ListItem key={key}>
                                <ListItemButton 
                                    onClick={() => navigate(path)}
                                    sx={{
                                        backgroundColor: isActive(path) ? "primary.light" : "transparent",
                                        borderRadius: 1,
                                        mx: 1,
                                        "&:hover": {
                                            backgroundColor: isActive(path) ? "primary.main" : "action.hover"
                                        },
                                        "&.Mui-selected": {
                                            backgroundColor: "primary.light",
                                            "&:hover": {
                                                backgroundColor: "primary.main"
                                            }
                                        }
                                    }}
                                    selected={isActive(path)}
                                >
                                    <ListItemIcon>
                                        <Icon sx={{ 
                                            color: isActive(path) ? "primary.dark" : "primary.main" 
                                        }} />
                                    </ListItemIcon>
                                    <ListItemText 
                                        primary={label} 
                                        sx={{ 
                                            color: isActive(path) ? "primary.dark" : "primary.dark",
                                            "& .MuiListItemText-primary": {
                                                fontWeight: isActive(path) ? 600 : 400
                                            }
                                        }} 
                                    />
                                </ListItemButton>
                            </ListItem>
                        ))}
                    </List>
                </Box>
            </Drawer>
        </>
    );
};