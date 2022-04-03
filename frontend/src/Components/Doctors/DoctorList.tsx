import { DoctorData } from "../../Api/DoctorsData";
import { Doctor } from "./Doctor";

interface Props {
  data: DoctorData[] | undefined;
}

export const DoctorList = ({ data }: Props) => (
  <ul>
    {data?.map((doctor) => (
      <li key={doctor.id}>
        <Doctor doctor={doctor} />
      </li>
    ))}
  </ul>
);
