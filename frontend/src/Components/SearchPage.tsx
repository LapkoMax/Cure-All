/** @jsxImportSource @emotion/react */
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { DoctorData, getDoctors } from "../Api/DoctorsData";
import { forSearch } from "../Styles/SearchPageStyles";
import { DoctorList } from "./Doctors/DoctorList";
import { Page } from "./General/Page";

export const SearchPage = () => {
  const [searchParams] = useSearchParams();
  const [doctors, setDoctors] = useState<DoctorData[]>([]);
  const [doctorsLoaded, setDoctorsLoaded] = useState(false);
  const searchTerm = searchParams.get("fullNameSearchTerm") || "";

  useEffect(() => {
    const doSearch = async (fullNameSearchTerm: string) => {
      const results = await getDoctors({
        fullNameSearchTerm: fullNameSearchTerm,
      });
      setDoctors(results);
      setDoctorsLoaded(true);
    };
    doSearch(searchTerm);
  }, [searchTerm]);

  return (
    <Page title="Search Results">
      {searchTerm && <p css={forSearch}>for "{searchTerm}"</p>}
      {!doctorsLoaded ? <div>Loading...</div> : <DoctorList data={doctors} />}
    </Page>
  );
};
