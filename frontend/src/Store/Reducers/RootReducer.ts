import { combineReducers } from "redux";
import { doctorsReducer, DoctorsState } from "./DoctorReducer";

export interface AppState {
  readonly doctors: DoctorsState;
}

export const rootReducer = combineReducers<AppState>({
  doctors: doctorsReducer,
});
