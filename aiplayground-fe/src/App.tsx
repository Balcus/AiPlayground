import './App.css';
import { Outlet } from "react-router-dom";
import { createTheme, ThemeProvider } from '@mui/material';
import { Menu } from "./components/Menu"

function App() {

const theme = createTheme({
palette: {
  mode: 'light',
  primary: {
    light: '#fbffd4',
    main: '#c4d413',
    dark: '#2c3000',
    contrastText: '#fff',
  },
  secondary: {
    light: '#f8f7ff',
    main: '#f9dc87',
    dark: '#ba000d',
    contrastText: '#000',
  },
},
});

  return (
    <>
      <ThemeProvider theme={theme}>
        <Menu />
        <Outlet />
      </ThemeProvider>
    </>
  );
}

export default App;
