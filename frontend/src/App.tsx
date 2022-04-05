/** @jsxImportSource @emotion/react */
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import persistStore from "redux-persist/es/persistStore";
import { PersistGate } from "redux-persist/integration/react";
import { appContainer } from "./AppStyles";
import { DoctorPage } from "./Components/DoctorPage";
import { Header } from "./Components/General/Header";
import { HomePage } from "./Components/HomePage";
import { NotFoundPage } from "./Components/NotFoundPage";
import { SearchPage } from "./Components/SearchPage";
import { SignInPage } from "./Components/SignInPage";
import { configureStore } from "./Store/Store";

const store = configureStore();

const persistor = persistStore(store);

function App() {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <BrowserRouter>
          <div css={appContainer}>
            <Header />
            <Routes>
              <Route path="" element={<HomePage />} />
              <Route path="signin" element={<SignInPage />} />
              <Route path="search" element={<SearchPage />} />
              <Route path="doctors/:doctorId" element={<DoctorPage />} />
              <Route path="*" element={<NotFoundPage />} />
            </Routes>
          </div>
        </BrowserRouter>
      </PersistGate>
    </Provider>
  );
}

export default App;
