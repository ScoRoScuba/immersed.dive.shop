import { useParams } from "react-router-dom";



export default function EventDetails() {

    const {id} = useParams<{id: string}>()
}