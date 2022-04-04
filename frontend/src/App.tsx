/** @jsxImportSource @emotion/react */
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { appContainer } from "./AppStyles";
import { Header } from "./Components/General/Header";
import { HomePage } from "./Components/HomePage";
import { NotFoundPage } from "./Components/NotFoundPage";
import { SearchPage } from "./Components/SearchPage";
import { SignInPage } from "./Components/SignInPage";

function App() {
  return (
    <BrowserRouter>
      <div css={appContainer}>
        <Header />
        <Routes>
          <Route path="" element={<HomePage />} />
          <Route path="signin" element={<SignInPage />} />
          <Route path="search" element={<SearchPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
