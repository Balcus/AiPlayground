import { BrowserRouter } from 'react-router-dom';
import { createRoot } from 'react-dom/client';
import { AppRoutes } from './configs/AppRoutes';

const conatiner = document.getElementById("root");

if (conatiner) {
  const root = createRoot(conatiner);

  root.render(
  <BrowserRouter>
    <AppRoutes />
  </BrowserRouter>
  );
}