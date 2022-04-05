import { combineReducers } from "redux";
import { doctorsReducer, DoctorsState } from "./DoctorReducer";
import { identityReducer, IdentityState } from "./IdentityReducer";

export interface AppState {
  readonly doctors: DoctorsState;
  readonly identity: IdentityState;
}

export const rootReducer = combineReducers<AppState>({
  doctors: doctorsReducer,
  identity: identityReducer,
});
