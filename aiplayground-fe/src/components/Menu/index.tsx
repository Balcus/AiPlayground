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
import { useNavigate } from "react-router-dom";
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

    const toggleDrawer = (state: boolean) => () => {
    setOpen(state);
    };

    return (
        <>
            {!open && (
            <IconButton onClick={toggleDrawer(true)} sx={{ position: "absolute", top: 16, left: 16, zIndex: 1300 }}>
                <MenuIcon sx={{ color: "primary.main" }} />
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
                            src="public/assets/logo_simple_transparent.png"
                            className="menu-logo-image"
                            alt=""
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
                        <ListItem key="home">
                            <ListItemButton onClick={() => navigate("/")}>
                                <ListItemIcon>
                                    <HomeOutlinedIcon sx={{ color: "primary.main" }} />
                                </ListItemIcon>
                                <ListItemText primary="Home" sx={{ color: "primary.dark" }} />
                            </ListItemButton>
                        </ListItem>

                        <ListItem key="platforms">
                            <ListItemButton onClick={() => navigate("/platforms")}>
                                <ListItemIcon>
                                    <DevicesIcon sx={{ color: "primary.main" }} />
                                </ListItemIcon>
                                <ListItemText primary="Platforms" sx={{ color: "primary.dark" }} />
                            </ListItemButton>
                        </ListItem>

                        <ListItem key="models">
                            <ListItemButton onClick={() => navigate("/models")}>
                                <ListItemIcon>
                                    <SmartToyOutlinedIcon sx={{ color: "primary.main" }} />
                                </ListItemIcon>
                                <ListItemText primary="Models" sx={{ color: "primary.dark" }} />
                            </ListItemButton>
                        </ListItem>

                        <ListItem key="runs">
                            <ListItemButton onClick={() => navigate("/runs")}>
                                <ListItemIcon>
                                    <WysiwygRoundedIcon sx={{ color: "primary.main" }} />
                                </ListItemIcon>
                                <ListItemText primary="Runs" sx={{ color: "primary.dark" }} />
                            </ListItemButton>
                        </ListItem>

                        <ListItem key="scopes">
                            <ListItemButton onClick={() => navigate("/scopes")}>
                                <ListItemIcon>
                                    <FolderCopyOutlinedIcon sx={{ color: "primary.main" }} />
                                </ListItemIcon>
                                <ListItemText primary="Scopes" sx={{ color: "primary.dark" }} />
                            </ListItemButton>
                        </ListItem>

                        <ListItem key="prompts">
                            <ListItemButton onClick={() => navigate("/prompts")}>
                                <ListItemIcon>
                                    <SpeakerNotesOutlinedIcon sx={{ color: "primary.main" }} />
                                </ListItemIcon>
                                <ListItemText primary="Prompts" sx={{ color: "primary.dark" }} />
                            </ListItemButton>
                        </ListItem>
                    </List>
                </Box>
            </Drawer>
        </>
    );
};