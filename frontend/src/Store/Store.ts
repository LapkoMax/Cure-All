import { createStore, Store } from "redux";
import { AppState, rootReducer } from "./Reducers/RootReducer";

export function configureStore(): Store<AppState> {
  const store = createStore(rootReducer, undefined);
  return store;
}
