import {
  Box,
  Card,
  CardContent,
  CardMedia,
  CircularProgress,
  Grid,
  List,
  ListItem,
  ListItemText,
  Paper,
  Stack,
  Typography,
  LinearProgress,
  Chip,
  Divider,
} from "@mui/material";
import { PlatfromsApiClient } from "../../api/Clients/PlatformsApiClient";
import { ModelModel } from "../../api/Models/ModelModel";
import { PlatformModel } from "../../api/Models/PlatformModel";
import { Model } from "../Shared/Types/Model";
import { Platform } from "../Shared/Types/Platform";
import "./Platforms.css";
import { FC, useEffect, useState } from "react";

export const Platforms: FC = () => {
  const [isLoading, setLoading] = useState(false);
  const [platforms, setPlatforms] = useState<Platform[]>([]);

  const fetchPlatforms = async () => {
    try {
      setLoading(true);
      const res = await PlatfromsApiClient.getAllAsync();
      const fetchedPlatforms = res.map(
        (e: PlatformModel): Platform => ({
          id: e.id,
          name: e.name,
          imageUrl: e.imageUrl,
          models: e.models.map(
            (model: ModelModel): Model => ({
              id: model.id,
              name: model.name,
              averageRating: model.averageRating,
            })
          ),
        })
      );
      setLoading(false);
      setPlatforms(fetchedPlatforms);
    } catch (error: any) {
      console.log(error);
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPlatforms();
  }, []);

  const getScoreColor = (
    score: number
  ): "success" | "info" | "warning" | "error" => {
    if (score >= 90) return "success";
    if (score >= 70) return "info";
    if (score >= 50) return "warning";
    return "error";
  };

  const getScoreLabel = (score: number): string => {
    if (score >= 90) return "Excellent";
    if (score >= 70) return "Good";
    if (score >= 50) return "Fair";
    return "Poor";
  };

  const getAveragePlatformRating = (models: Model[]): number => {
    if (models.length === 0) return 0;
    const sum = models.reduce((acc, model) => acc + model.averageRating, 0);
    return Math.round(sum / models.length);
  };

  if (isLoading) {
    return (
      <Stack justifyContent="center" alignItems="center" height="80vh">
        <CircularProgress size={60} />
      </Stack>
    );
  }

  return (
    <Box p={4} sx={{ minHeight: "100vh" }}>
      <Grid container spacing={4}>
        {platforms.map((platform) => {
          const avgRating = getAveragePlatformRating(platform.models);
          return (
            <Grid size={{ xs: 12, sm: 6, md: 4 }} key={platform.id}>
              <Card
                className="platforms-card"
                sx={{
                  height: "100%",
                  display: "flex",
                  flexDirection: "column",
                  transition: "all 0.3s ease-in-out",
                  "&:hover": {
                    transform: "translateY(-4px)",
                    boxShadow: 6,
                  },
                }}
              >
                <CardMedia
                  component="img"
                  height="200"
                  src={platform.imageUrl}
                  alt={platform.name || "Platform"}
                  className="platforms-card-media"
                  sx={{ objectFit: "cover" }}
                />

                <CardContent sx={{ flexGrow: 1, p: 3 }}>
                  <Box sx={{ mb: 2 }}>
                    <Stack
                      direction="row"
                      justifyContent="space-between"
                      alignItems="center"
                      sx={{ mb: 1 }}
                    >
                      <Typography
                        variant="h5"
                        component="div"
                        fontWeight="bold"
                      >
                        {platform.name || "Unknown Platform"}
                      </Typography>
                      <Chip
                        label={`${avgRating}/100`}
                        color={getScoreColor(avgRating)}
                        sx={{ fontWeight: "bold" }}
                      />
                    </Stack>

                    <Box sx={{ mb: 1 }}>
                      <LinearProgress
                        variant="determinate"
                        value={avgRating}
                        color={getScoreColor(avgRating)}
                        sx={{
                          height: 8,
                          borderRadius: 4,
                          backgroundColor: "rgba(0,0,0,0.1)",
                        }}
                      />
                    </Box>

                    <Stack
                      direction="row"
                      justifyContent="space-between"
                      alignItems="center"
                    >
                      <Typography variant="body2" color="text.secondary">
                        Platform Average: {getScoreLabel(avgRating)}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        {platform.models.length} models
                      </Typography>
                    </Stack>
                  </Box>

                  <Divider sx={{ my: 2 }} />

                  <Paper
                    variant="outlined"
                    sx={{
                      maxHeight: 280,
                      overflow: "auto",
                      backgroundColor: "#fafafa",
                    }}
                  >
                    <List dense sx={{ p: 0 }}>
                      {platform.models.map((model, index) => (
                        <Box key={model.id}>
                          <ListItem sx={{ px: 2, py: 1.5 }}>
                            <Box sx={{ width: "100%" }}>
                              <Stack
                                direction="row"
                                justifyContent="space-between"
                                alignItems="center"
                                sx={{ mb: 1 }}
                              >
                                <Typography
                                  variant="subtitle2"
                                  fontWeight="medium"
                                >
                                  {model.name}
                                </Typography>
                                <Chip
                                  label={`${model.averageRating}`}
                                  size="small"
                                  color={getScoreColor(model.averageRating)}
                                  variant="outlined"
                                />
                              </Stack>

                              <Box sx={{ mb: 0.5 }}>
                                <LinearProgress
                                  variant="determinate"
                                  value={model.averageRating}
                                  color={getScoreColor(model.averageRating)}
                                  sx={{
                                    height: 6,
                                    borderRadius: 3,
                                    backgroundColor: "rgba(0,0,0,0.08)",
                                  }}
                                />
                              </Box>

                              <Typography
                                variant="caption"
                                color="text.secondary"
                              >
                                {getScoreLabel(model.averageRating)} Performance
                              </Typography>
                            </Box>
                          </ListItem>
                          {index < platform.models.length - 1 && <Divider />}
                        </Box>
                      ))}
                    </List>
                  </Paper>
                </CardContent>
              </Card>
            </Grid>
          );
        })}
      </Grid>
    </Box>
  );
};
