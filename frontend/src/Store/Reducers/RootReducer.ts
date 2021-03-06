import { combineReducers } from "redux";
import { appointmentsReducer, AppointmentsState } from "./AppointmentReducer";
import { doctorsReducer, DoctorsState } from "./DoctorReducer";
import { identityReducer, IdentityState } from "./IdentityReducer";
import { notificationReducer, NotificationsState } from "./NotificationReducer";
import { patientCardsReducer, PatientCardsState } from "./PatientCardReducer";
import { patientsReducer, PatientsState } from "./PatientReducer";

export interface AppState {
  readonly doctors: DoctorsState;
  readonly patients: PatientsState;
  readonly appointments: AppointmentsState;
  readonly patientCards: PatientCardsState;
  readonly notifiactions: NotificationsState;
  readonly identity: IdentityState;
}

export const rootReducer = combineReducers<AppState>({
  doctors: doctorsReducer,
  patients: patientsReducer,
  appointments: appointmentsReducer,
  patientCards: patientCardsReducer,
  notifiactions: notificationReducer,
  identity: identityReducer,
});
