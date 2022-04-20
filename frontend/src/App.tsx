/** @jsxImportSource @emotion/react */
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import persistStore from "redux-persist/es/persistStore";
import { PersistGate } from "redux-persist/integration/react";
import { Header } from "./Components/General/Header";
import { RoutesComponent } from "./Components/Pages/General/RoutesComponent";
import { configureStore } from "./Store/Store";

const store = configureStore();

const persistor = persistStore(store);

function App() {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <BrowserRouter>
          <Header />
          <RoutesComponent />
        </BrowserRouter>
      </PersistGate>
    </Provider>
  );
}

export default App;
