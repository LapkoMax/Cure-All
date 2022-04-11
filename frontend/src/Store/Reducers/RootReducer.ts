import { combineReducers } from "redux";
import { appointmentsReducer, AppointmentsState } from "./AppointmentReducer";
import { doctorsReducer, DoctorsState } from "./DoctorReducer";
import { identityReducer, IdentityState } from "./IdentityReducer";

export interface AppState {
  readonly doctors: DoctorsState;
  readonly appointments: AppointmentsState;
  readonly identity: IdentityState;
}

export const rootReducer = combineReducers<AppState>({
  doctors: doctorsReducer,
  appointments: appointmentsReducer,
  identity: identityReducer,
});
