/** @jsxImportSource @emotion/react */
import { useForm } from "react-hook-form";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import {
  headerContainer,
  searchInput,
  signInAnchor,
  titleAnchor,
} from "../../Styles/General/HeaderStyles";
import { UserIcon } from "./Icons";

type FormData = {
  searchTerm: string;
};

export const Header = () => {
  const { register, handleSubmit } = useForm<FormData>();
  const [searchParams] = useSearchParams();
  const searchTerm = searchParams.get("fullNameSearchTerm") || "";
  const navigate = useNavigate();

  const submitForm = ({ searchTerm }: FormData) => {
    searchTerm = searchTerm.trim();
    if (searchTerm !== "") navigate("search?fullNameSearchTerm=" + searchTerm);
  };

  return (
    <div css={headerContainer}>
      <Link to="/" css={titleAnchor}>
        Cure-All
      </Link>
      <form onSubmit={handleSubmit(submitForm)}>
        <input
          {...register("searchTerm")}
          type="text"
          placeholder="Search for doctor..."
          defaultValue={searchTerm}
          css={searchInput}
        />
      </form>
      <Link to="signin" css={signInAnchor}>
        <UserIcon />
        <span>Sign In</span>
      </Link>
    </div>
  );
};
