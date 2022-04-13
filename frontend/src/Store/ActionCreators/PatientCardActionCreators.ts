import { PatientCardData } from "../../Api/PatientCardsData";
import {
  GETTINGPATIENTCARD,
  GOTPATIENTCARD,
} from "../Actions/PatientCardActions";

export const gettingPatientCardAction = () =>
  ({
    type: GETTINGPATIENTCARD,
  } as const);

export const gotPatientCardAction = (patientCard: PatientCardData | null) =>
  ({
    type: GOTPATIENTCARD,
    patientCard: patientCard,
  } as const);
