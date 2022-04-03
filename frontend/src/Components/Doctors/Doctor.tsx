import { DoctorData } from "../../Api/DoctorsData";

interface Props {
  doctor: DoctorData;
}

export const Doctor = ({ doctor }: Props) => (
  <div>
    <div>
      {doctor.firstName} {doctor.lastName}
    </div>
    <div>Speciality: {doctor.speciality}</div>
    <div>Years of experience: {doctor.yearsOfExperience}</div>
  </div>
);
