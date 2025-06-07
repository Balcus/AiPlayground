import { FC } from "react";
import { Routes, Route } from "react-router-dom";
import App from "../App";
import { Home } from "../components/Home";
import { Platforms } from "../components/Platforms";
import { Models } from "../components/Models";
import { Runs } from "../components/Runs";
import { Prompts } from "../components/Prompts";
import { Scopes } from "../components/Scopes";
import { ManagePrompt } from "../components/Prompts/ManagePrompt";

export const AppRoutes: FC = () => {
  return (
    <Routes>
      <Route path={"/"} element={<App />}>
        <Route path={"/"} element={<Home />} />
        <Route path={"/platforms"} element={<Platforms />} />
        <Route path={"/models"} element={<Models />} />
        <Route path={"/runs"} element={<Runs />} />
        <Route path={"/prompts"}>
          <Route index={true} element={<Prompts />} />
          <Route path={"create"} element={<ManagePrompt />} />
          <Route path={"view/:id"} element={<ManagePrompt />} />
        </Route>
        <Route path={"/scopes"} element={<Scopes />}></Route>
        <Route path={"*"} element={<div>Not found</div>}></Route>
      </Route>
    </Routes>
  );
};
