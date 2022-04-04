import { DoctorData } from "../../Api/DoctorsData";
import {
  GETTINGDOCTOR,
  GETTINGDOCTORS,
  GOTDOCTOR,
  GOTDOCTORS,
} from "../Actions/DoctorActions";

export const gettingDoctorsAction = () =>
  ({
    type: GETTINGDOCTORS,
  } as const);

export const gotDoctorsAction = (doctors: DoctorData[]) =>
  ({
    type: GOTDOCTORS,
    doctors: doctors,
  } as const);

export const gettingDoctorAction = () =>
  ({
    type: GETTINGDOCTOR,
  } as const);

export const gotDoctorAction = (doctor: DoctorData | null) =>
  ({
    type: GOTDOCTOR,
    doctor: doctor,
  } as const);
