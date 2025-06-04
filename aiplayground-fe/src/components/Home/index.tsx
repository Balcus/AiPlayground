import { FC } from "react";
import { Box, Button } from "@mui/material";
import "./Home.css";

export const Home: FC = () => {
    return (
        <Box className="bg">
            <Box className="call-to-action">
                <img className="illustration" src="./assets/cute-robot.png" alt="Cute robot" />
                <Box className="content">
                    <h1>Wanna break this robot's heart?</h1>
                    <p>
                        Well you better do it because he's an evil AI that will take your job soon!
                        Make it up for it and feel better at the same time by rating his little stupid responses.
                        At least have a bit of fun until the day we become extinct as programmers!
                    </p>
                    <Button variant="contained">Learn how</Button>
                </Box>
            </Box>
        </Box>
    );
};