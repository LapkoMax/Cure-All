import { PatientData } from "../../Api/PatientsData";
import { GETTINGPATIENT, GOTPATIENT } from "../Actions/PatientActions";

export const gettingPatientAction = () =>
  ({
    type: GETTINGPATIENT,
  } as const);

export const gotPatientAction = (patient: PatientData | null) =>
  ({ type: GOTPATIENT, patient: patient } as const);
