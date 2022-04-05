import { createStore } from "redux";
import { AppState, rootReducer } from "./Reducers/RootReducer";
import storage from "redux-persist/lib/storage";
import { persistReducer } from "redux-persist";

const persistConfig = {
  key: "root",
  storage,
};

const persistedReducer = persistReducer<AppState>(persistConfig, rootReducer);

export function configureStore() {
  const store = createStore(persistedReducer);
  return store;
}
